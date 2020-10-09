#!/bin/bash
runId=${RUN_ID:-0.1.0}
timeout=${SETUP_TIMEOUT:-600}
timestamp=$(date +%d-%m-%Y-%H-%M-%S)
CURLPBURL=$(echo $PBURL | sed 's:/*$::')

setAdditionalArgs () {
  additionalDotnetArgs=""  

  if [ -n "${TEST_RESULT_DIR}" ]; then
    >&2 echo "Directing test results to '$TEST_RESULT_DIR'"  
    additionalDotnetArgs+="--logger \"trx;LogFileName=of-$runId-$timestamp.trx\" --results-directory $TEST_RESULT_DIR "
  fi

  echo $additionalDotnetArgs
  return 0
}

echo "Waiting for $CURLPBURL to be ready..."

n=0
until [ "$n" -ge "$timeout" ]; do
  httpStatusCode=$(curl -I -s --insecure $CURLPBURL | cat | head -n 1 | cut -d" " -f2)

  if [ "$httpStatusCode" = "200" ]; then echo "$CURLPBURL was ready in $n seconds" && break; fi
  n=$((n+1)) 
  sleep 1
done

if [ "$n" -eq "$timeout" ]; then echo "$CURLPBURL is not ready after $n seconds" && exit 1; fi



if [ -n "${TEST_FILTER}" ]; then
  cmd="dotnet test out/MarketingPageAcceptanceTests.SmokeTests.dll -v n $(setAdditionalArgs smoke-tests)"
  echo -e "\n Running '$cmd' \n"
  eval $cmd
else
  cmd="dotnet test out/OrderFormAcceptanceTests.Tests.dll -v n"
  echo -e "\n Running '$cmd' \n"
  eval $cmd
fi
