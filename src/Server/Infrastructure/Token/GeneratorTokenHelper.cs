namespace Server.Infrastructure.Token;

public class GeneratorTokenHelper : IGeneratorTokenHelper
{
    #region Fields
    private readonly IConfiguration configuration;
    #endregion /Fields

    #region Constructure
    public GeneratorTokenHelper(IConfiguration configuration) : base()
    {
        this.configuration = configuration;
    }
    #endregion /Constructure

    #region Methods
    public string GenerateToken(TokenUserViewModel viewModel)
    {
        if (viewModel is null)
        {
            throw new ArgumentNullException(nameof(viewModel));
        }

        var key =
            configuration["JwtSettings:Key"];

        var expireTime =
            configuration["JwtSettings:ExpireTime"];

        if (key is null || expireTime is null)
        {
            throw new ArgumentNullException($"{nameof(key)}  {nameof(expireTime)}");
        }

        var securityKey =
            new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));

        var credentials =
           new SigningCredentials(key: securityKey,
                algorithm: SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(type:ClaimTypes.Name,value:viewModel.FullName),
            new Claim(type:ClaimTypes.Role,value:viewModel.Role),
            new Claim(type:nameof(viewModel.UserName),value:viewModel.UserName),
        };

        var securityToken =
            new JwtSecurityToken(
                signingCredentials: credentials,
                    claims: claims, expires: DateTime.UtcNow.AddMinutes(value: int.Parse
                      (expireTime)));

        var token =
           new JwtSecurityTokenHandler().WriteToken(token: securityToken);

        return token;
    }
    #endregion /Methods
}
