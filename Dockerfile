FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11
ENV MSBUILDSINGLELOADCONTEXT=1
WORKDIR /app
COPY ./src ./entrypoint.sh ./
RUN apk add --no-cache bash && apk add curl
RUN dotnet publish -c Release -o out
ENTRYPOINT ["/bin/bash", "entrypoint.sh"]