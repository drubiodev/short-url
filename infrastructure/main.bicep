param location string = resourceGroup().location // getting location of the resource group

var id = uniqueString(resourceGroup().id)

module apiService 'modules/compute/appservice.bicep' = {
  name: 'apiDeployment'
  params: {
    location: location
    appServicePlanName: 'plan-api-${id}'
    appName: 'api-${id}'
  }
}
