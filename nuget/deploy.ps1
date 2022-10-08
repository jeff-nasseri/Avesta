param($version)

#delete all nuget packages
del .\Debug\
cd ..
dotnet clean
dotnet build
cd .\nuget\Debug\


#cd .\Debug\


$nuget_key = oy2l63lkkwuxkoqf2gwqtkpecbtx5bje7hqvi6vzpv4pjq


$source_path = "."
$filter = "*.nupkg"

$files = Get-ChildItem -Recurse -Path $source_path -Filter $filter

    ForEach ($file in $files) {
        echo("`n==========`n")
        dotnet nuget push $file.Name --api-key $nuget_key --source https://api.nuget.org/v3/index.json --skip-duplicate
        echo("`n==========`n")
    } 



