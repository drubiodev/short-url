param location string = resourceGroup().location // getting location of the resource group

var id = uniqueString(resourceGroup().id)

module keyVault 'modules/secrets/keyvault.bicep' = {
  name: 'keyVaultDeployment'
  params: {
    location: location
    vaultName: 'kv-${id}'
  }
}

module apiService 'modules/compute/appservice.bicep' = {
  name: 'apiDeployment'
  params: {
    location: location
    appServicePlanName: 'plan-api-${id}'
    appName: 'api-${id}'
    keyvaultName: keyVault.outputs.name
  }
  dependsOn: [
    keyVault
  ]
}

module keyVaultRoleAssignment 'modules/secrets/keyvault-role-assignment.bicep' = {
  name: 'keyVaultRoleAssignmentDeployment'
  params: {
    keyVaultName: keyVault.outputs.name
    principalIds: [
      apiService.outputs.principalId
    ]
  }
  dependsOn: [
    apiService
    apiService
  ]
}
