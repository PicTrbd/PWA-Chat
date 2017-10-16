############################################
# PROVIDER
############################################

provider "azurerm" {
    subscription_id = "${var.azure_subscription_id}"
    client_secret = "${var.azure_client_secret}"
    client_id = "${var.azure_client_id}"
    tenant_id = "${var.azure_tenant_id}"
    skip_provider_registration = "true"
}


############################################
# RESOURCES
############################################

resource "azurerm_resource_group" "production" {
    name = "pwachatpushterraform"
    location = "West Europe"
}

resource "azurerm_app_service_plan" "production" {
  name                = "service-plan-pwa"
  location            = "${azurerm_resource_group.production.location}"
  resource_group_name = "${azurerm_resource_group.production.name}"

  sku {
    tier = "Basic"
    size = "B1"
  }
}

resource "azurerm_app_service" "Web-app" {
    name = "pwachatpush-webapp"
    location = "${azurerm_resource_group.production.location}"
    resource_group_name = "${azurerm_resource_group.production.name}"
    app_service_plan_id = "${azurerm_app_service_plan.production.id}"

    site_config {
        dotnet_framework_version = "v4.0"
    }
}

resource "azurerm_app_service" "API" {
    name = "pwachatpush-rest-api"
    location = "${azurerm_resource_group.production.location}"
    resource_group_name = "${azurerm_resource_group.production.name}"
    app_service_plan_id = "${azurerm_app_service_plan.production.id}"

    site_config {
        dotnet_framework_version = "v4.0"
    }

    app_settings {
    "DbAuthentication:UserId" = "${var.UserId}"
    "DbAuthentication:Password" = "${var.Password}"
    "DbAuthentication:Database" = "${var.Database}"
    "DbAuthentication:ServerURL" = "${var.ServerURL}"
  }
}


############################################
# OUTPUT
############################################