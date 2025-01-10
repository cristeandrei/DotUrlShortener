set -x

echo "setup dotnet tools"

dotnet tool install --global Microsoft.OpenApi.Kiota

dotnet tool install --global csharpier

dotnet dev-certs https --trust

sudo dotnet workload update

sudo dotnet workload install aspire
