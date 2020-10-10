# Generate DbContext

- Install-Package Microsoft.EntityFrameworkCore.Tools

- Scaffold-DbContext "Data source=10.10.7.35;Initial Catalog=NatFlutter;User ID=saranpong;Password=saranpong;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force

- edit dbcontext to use connectionstring

	   private IConfiguration _config;

	   public NatFlutterContext(IConfiguration config)
	   {
	       _config = config;
	   }
	   
	   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	   {
	       if (!optionsBuilder.IsConfigured)
	       {
	           optionsBuilder.UseSqlServer(_config["ConnectionStrings:NatFlutterContext"]);
	       }
	   }

