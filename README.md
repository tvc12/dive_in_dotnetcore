### Dive in Dotnet Core

### Getting start

+ Install Dotnet Core (version 3.1):

```bash
wget -q https://packages.microsoft.com/config/ubuntu/19.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-3.1
```

**Warning**

If you receive an error message similar to **Unable to locate package dotnet-sdk-3.1**, see the [Troubleshoot the package manager section](https://docs.microsoft.com/vi-vn/dotnet/core/install/linux-package-manager-ubuntu-1904#troubleshoot-the-package-manager).