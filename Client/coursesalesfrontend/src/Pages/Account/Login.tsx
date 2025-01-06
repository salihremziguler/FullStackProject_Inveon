import React, { useState } from "react";
import { useSignInMutation } from "../../Api/accountApi";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { LoginModel } from "../../interfaces/loginModel";
import { setLoggedInUser } from "../../Storage/Redux/authenticationSlice";
import jwtDecode from "jwt-decode";
import "./Styles/Login.css";

const Login: React.FC = () => {
  const [userData, setUserDataState] = useState({
    usernameOrEmail: "",
    password: "",
  });

  const navigate = useNavigate();
  const [userSignInMutation] = useSignInMutation();
  const dispatch = useDispatch();

  const handleLoginSubmit = async () => {
    try {
      const response = await userSignInMutation(userData).unwrap();

      if (response.token) {
        const { accessToken } = response.token;
        localStorage.setItem("token", accessToken);

        const decoded: any = jwtDecode(accessToken);
        const name = decoded.name;
        const role = decoded.role;

        dispatch(
          setLoggedInUser({
            name,
            role,
          })
        );

        navigate("/");
      } else {
        console.error("Login failed: Token not found in response");
      }
    } catch (error) {
      console.error("Login failed:", error);
    }
  };

  return (
    <section className="login-section">
      <div className="login-container">
        <div className="login-left">
          <img
            src="/assets/login-image.png"
            alt="Login Visual"
            className="login-image"
          />
        </div>
        <div className="login-right">
          <h1 className="login-title">
            Öğrenim yolculuğunuza devam etmek için oturum açın
          </h1>
          <form className="login-form">
            <input
              type="text"
              className="login-input"
              placeholder="E-posta"
              value={userData.usernameOrEmail}
              onChange={(e) =>
                setUserDataState((prevState) => ({
                  ...prevState,
                  usernameOrEmail: e.target.value,
                }))
              }
            />
            <input
              type="password"
              className="login-input"
              placeholder="Şifre"
              value={userData.password}
              onChange={(e) =>
                setUserDataState((prevState) => ({
                  ...prevState,
                  password: e.target.value,
                }))
              }
            />
            <button
              onClick={handleLoginSubmit}
              type="button"
              className="login-button"
            >
              <span>📧</span> Oturum Aç
            </button>
          </form>
        </div>
      </div>
    </section>
  );
};

export default Login;
