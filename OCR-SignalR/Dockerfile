FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY publish/ocr-signalr .
COPY seft-cert.crt /usr/local/share/ca-certificates
RUN update-ca-certificates
ENTRYPOINT ["dotnet", "OCR-SignalR.dll"]
