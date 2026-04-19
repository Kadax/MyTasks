
Write-Host "Build Front" -ForegroundColor Green

cd .\MyTask\

docker build --network=host -t mytask-front-docker .
docker tag mytask-front-docker:latest otherdomain.ru:5000/mytask-front-docker:latest
docker push otherdomain.ru:5000/mytask-front-docker:latest

cd ..

Write-Host "Build BackEnd" -ForegroundColor Yellow

docker build --network=host -f MyTaskAPI/Dockerfile -t mytask-backend-docker .
docker tag mytask-backend-docker:latest otherdomain.ru:5000/mytask-backend-docker:latest
docker push otherdomain.ru:5000/mytask-backend-docker:latest
