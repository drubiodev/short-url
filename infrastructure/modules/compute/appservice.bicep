param location string = resourceGroup().location // getting location of the resource group
param appServicePlanName string
param appName string
param keyvaultName string

resource appServicePlan 'Microsoft.Web/serverfarms@2024-04-01' = {
  kind: 'linux'
  location: location
  name: appServicePlanName
  properties: {
    reserved: true
  }
  sku: {
    name: 'B1'
  }
}

resource webApp 'Microsoft.Web/sites@2024-04-01' = {
  name: appName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|9.0'
      appSettings: [
        {
          name: 'KeyVaultName'
          value: keyvaultName
        }
      ]
    }
  }
}

resource webbAppConfig 'Microsoft.Web/sites/config@2024-04-01' = {
  parent: webApp
  name: 'web'
  properties: {
    scmType: 'GitHub'
  }
}

output appServiceId string = webApp.id
output principalId string = webApp.identity.principalId
