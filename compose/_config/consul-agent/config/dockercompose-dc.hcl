log_level = "DEBUG"
datacenter=  "dockercompose-dc"
server = false

bootstrap_expect = 0
ui = true

connect {
  enabled = true
}

ports {
  grpc = 8502
}

#advertise_addr = "consul"
enable_central_service_config = true
telemetry {
  prometheus_retention_time = "10s"
}
