using APPC.Models.Auth;
using APPC.Models.Common;
using APPC.Repositories.Interfaces;
using APPC.Services;
using System.Data;
using System.Data.SqlClient;

namespace APPC.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly string _connectionString;
        private readonly IPasswordHelperService _passwordHelperService;

        public AuthRepository(IConfiguration configuration, IPasswordHelperService passwordHelperService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _passwordHelperService = passwordHelperService;
        }

        public async Task<ServiceResult<LoginResponseDto>> LoginRepoAsync(LoginRequestDto model)
        {
            DataTable dt = await GetUserAsyc(model.Email);

            if (dt.Rows.Count == 0)
            {
                return ServiceResult<LoginResponseDto>.Fail(StatusCodes.Status401Unauthorized, "user not found.");
            }

            string? storedSalt = dt.Rows[0]["PasswordSalt"].ToString();
            string? storedHash = dt.Rows[0]["PasswordHash"].ToString();

            if(string.IsNullOrEmpty(storedSalt) || string.IsNullOrEmpty(storedHash))
            {
                return ServiceResult<LoginResponseDto>.Fail(StatusCodes.Status401Unauthorized,"password not set for user");
            }

            bool verifyPassword = _passwordHelperService.VerifyPasswordHash(model.Password, storedHash, storedSalt);
            
            if(!verifyPassword)
            {
                return ServiceResult<LoginResponseDto>.Fail(StatusCodes.Status401Unauthorized, "Invalid password");
            }

            // Assuming you have a method to generate a JWT token or similar
            string token = "generated_token_here";

            var response = new LoginResponseDto
            {
                Token = token,
                Email = model.Email,
                FullName = dt.Rows[0]["FullName"].ToString(),
                PlatformRole = dt.Rows[0]["PlatformRole"].ToString(),
                UserId = dt.Rows[0]["UserId"].ToString(),
            };

            return ServiceResult<LoginResponseDto>.Ok(response, "Login successful");
        }

        private async Task<DataTable> GetUserAsyc(string Email)
        {
            // return new ApiResponse<object>(true, "");

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_get_user_by_email", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", Email);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
