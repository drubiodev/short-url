# URL Shortener

## Azure CLI

### Login to Azure

```bash
az login
```

## Create a Resource Group

```bash
az deployment group create --resource-group shorturl-dev-rg --template-file infrastructure\main.bicep
```

### Deploy bicep script

```bash
az deployment create 
```

#### Deploy what-if

result will be a prediction what will happen

```bash
az deployment group what-if --resource-group shorturl-dev-rg --template-file infrastructure\main.bicep
```