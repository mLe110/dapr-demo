# dapr-demo

When running dapr on Mac using Colima, run the following command before starting dapr:
```bash
export DOCKER_HOST="unix://${HOME}/.colima/default/docker.sock"
```

Run the app with
```bash
dapr run --app-id order-workflow --app-port 5065 --dapr-http-port 3500 --resources-path ./ResourcesLocal dotnet run
```