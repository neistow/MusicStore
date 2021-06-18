set startup-path=Basket.Api.csproj
dotnet ef database drop -s %startup-path% --force
dotnet ef database update -s %startup-path%
pause
