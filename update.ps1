
Write-Host "Build Front" -ForegroundColor Green

docker build -f MyTask/Dockerfile -t mytask-front-docker .
docker tag mytask-front-docker otherdomain.ru:5000/mytask-front-docker
docker push otherdomain.ru:5000/mytask-front-docker


Write-Host "Build BackEnd" -ForegroundColor Yellow

docker build -f MyTaskAPI/Dockerfile -t mytask-backend-docker .
docker tag mytask-backend-docker otherdomain.ru:5000/mytask-backend-docker
docker push otherdomain.ru:5000/mytask-backend-docker
