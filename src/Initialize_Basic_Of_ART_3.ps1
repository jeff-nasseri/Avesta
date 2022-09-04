param($old_name,$replace_name)

  Get-ChildItem -Path . -Directory | ForEach-Object -Process {Rename-item -Path $_.Name -NewName ($_.name -replace $old_name,$replace_name) }


        $source_path = "."
        $filter = "*.csproj"

        $files = Get-ChildItem -Recurse -Path $source_path -Filter $filter

            ForEach ($file in $files) {
                
                $old_file_name = $file.Name
                $new_full_name = "$($file.DirectoryName)" + "\" + "$($old_file_name)".Replace($old_name,$replace_name)
                Rename-Item $file.FullName -NewName $new_full_name
            } 