# dapr-demo

When running dapr on Mac using Colima, run the following command before starting dapr:
```bash
export DOCKER_HOST="unix://${HOME}/.colima/default/docker.sock"
```

Run the app with
```bash
dapr run --app-id order-workflow --app-port 5270 --dapr-http-port 3500 --resources-path ./ResourcesLocal dotnet run
```
!Important: The app port in the command must be aligned with the port configured in the launchSettings.json

Get insights into the dapr instance using the dapr dashboard:
```bash
dapr dashboard
```