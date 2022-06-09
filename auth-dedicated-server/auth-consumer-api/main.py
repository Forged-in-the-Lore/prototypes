import uvicorn
from fastapi import Depends, FastAPI

from models import User
from user_validator import validate_user


def main():
    # Not sure about this one. Might be able to use a different scheme that doesn't provide a login endpoint
    app = FastAPI(dependencies=[Depends(validate_user)])

    @app.get("/me")
    async def read_users_me(current_user: User = Depends(validate_user)):
        """Endpoint to test if user ID is properly returned by token validator"""
        return current_user

    @app.get("/hello")
    async def say_hello():
        """Endpoint to test if token is properly verified by token validator"""
        return "Hello there. You have been validated!"

    uvicorn.run(app, host="0.0.0.0", port=8000)


if __name__ == '__main__':
    main()
