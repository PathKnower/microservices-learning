#clean up default namespace
kubectl delete --all deployments

#clean up nginx namespace
kubectl delete --all deployments --namespace=ingress-nginx