FROM microsoft/dotnet:2.1-sdk AS sdkImg
#set the current directory in the image (container) file system to src. create the folder if not exist.
WORKDIR /src/VehicleApi
#the below path for the source works if run from docker cli but might not work if run from inside project.
#since docker file and csproj are in the same directory, the path for the source should be like this.
#the destination is a folder in the image (container) file system.
COPY VehicleApi/VehicleApi.csproj .
#the below command will run from the new path where the csproj was copied.

RUN dotnet restore 
#copy all files into the src folder in the image (container) file system
COPY VehicleApi .

RUN dotnet publish VehicleApi.csproj -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runTimeImg
WORKDIR /app
#EXPOSE 62514
#EXPOSE 44369

EXPOSE 80

COPY --from=sdkImg /app .

ENTRYPOINT dotnet VehicleApi.dll
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet VehicleApi.dll