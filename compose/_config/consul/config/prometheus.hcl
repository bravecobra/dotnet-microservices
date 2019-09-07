services {
  name = "prometheus"
  port = 9090
  connect {
    sidecar_service {
      name = "prometheus-sidecar-proxy"
      check  {
          name = "Connect Sidecar Aliasing prometheus"
          alias_service = "prometheus"
      }
      proxy {
        local_service_address = "prometheus-sidecar"
        config {
          protocol = "http"
          bind_address = "prometheus-sidecar"
          bind_port = 9090
        }
        upstreams {
          destination_name = "prometheus"
          datacenter = "dockercompose-dc"
          local_bind_port = 9090
          local_bind_address = "prometheus-sidecar"
        }
      }
    }
  }
}
