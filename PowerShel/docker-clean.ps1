# Remove all images, containers & volumes
docker container stop $(docker container ls -a -q)
docker system prune -a -f --volumes