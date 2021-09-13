readonly baseUrl=https://3.144.84.254:6001/

json=no

login(){
	echo email
	read email
	echo password
	read password
	
	json=$(curl -X POST ${baseUrl}api/auth/login -H "Content-Type: application/json" \
		-d '{"email": "'${email}'","password": "'${password}'"}' --insecure)

	token=$(echo $json | jq .token)
	echo 
}

action=do
token=notoken
data=nodata

while([ $action != q ])
do
	echo Enter an action: login, register, authData, q
	read action
	
	if [ -z $action ]
	then
		exit
	fi

	case $action in
		"login")
			login
			;;
		"authData")
			echo $token
			echo $baseUrl
			curl -X GET $baseUrl/api/auth -H "Authorization: Bearer $token" --insecure
			;;
	esac
done

