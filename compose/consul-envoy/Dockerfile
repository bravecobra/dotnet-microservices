FROM consul:latest
FROM envoyproxy/envoy:v1.11.1
COPY --from=0 /bin/consul /bin/consul
ENTRYPOINT ["consul", "connect", "envoy"]