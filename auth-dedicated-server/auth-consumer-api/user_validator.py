import requests
from fastapi import HTTPException, Depends
from fastapi.security import OAuth2PasswordBearer

from models import User

oauth2_scheme = OAuth2PasswordBearer(tokenUrl="token")

auth_url = "https://localhost:7094/api/auth"


def validate_token(token):
    """Sends a JWT to the Authentication Service to verify that it was issues by said service and is valid. Raises
    unauthorized if the token isn't validated. """
    # verify is false to ignore https issues
    response = requests.post(url=auth_url, json={'token': token}, verify=False)
    if not response:
        raise HTTPException(status_code=401, detail="Cannot authenticate user. Please sign out and in.")
    return User(id=int(response.text))


async def validate_user(token: str = Depends(oauth2_scheme)):
    """Uses validate_token to verify the JWT token and returns user ID"""
    user = validate_token(token)
    return user
