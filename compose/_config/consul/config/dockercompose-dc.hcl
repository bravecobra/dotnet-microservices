log_level = "DEBUG"
datacenter=  "dockercompose-dc"
server = true

bootstrap_expect = 1
ui = true

connect {
  enabled = true
}

ports {
  grpc = 8502
}

#advertise_addr = "consul"
enable_central_service_config = true