# URL Shortener

## Azure CLI

### Login to Azure

```bash
az login
```

## Create a Resource Group run bicep file

```bash
az deployment group create --resource-group shorturl-rg-dev --template-file infrastructure/main.bicep
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
### Apply to custom role
used for writing roles when setting up azure in github actions

```bash
az ad sp create-for-rbac --name "Github-Actions-SP" --role infra_deploy --scopes /subscriptions/<SUBSCRIPTION_ID> --sdk-auth
```

### Create user fot GitHub Actions

```bash
az ad sp create-for-rbac --name "Github-Actions-SP" --role contributor --scopes /subscriptions/<SUBSCRIPTION_ID> --sdk-auth
```

### Configure Federated Credentials

search on how to add FIC to your app

### Get name of publish profile for web app
```bash
az webapp deployment list-publishing-profiles --name <NAME_OF_APP> --resource-group <NAME_OF_RESOURCEGROUP> --xml
```