import React, { useState } from "react";
import { useSignUpMutation } from "../../Api/userApi";
import { useNavigate } from "react-router-dom";

function Register() {
  const navigate = useNavigate();
  const [userData, setUserDataState] = useState({
    username: "",
    nameSurname: "",
    password: "",
    passwordConfirm: "",
    email: "",
  });

  const [userRegisterMutation] = useSignUpMutation();
  const [message, setMessage] = useState(""); // Başarı veya hata mesajı için

  const handleRegistrationSubmit = async () => {
    try {
      const response = await userRegisterMutation({
        username: userData.username,
        nameSurname: userData.nameSurname,
        password: userData.password,
        passwordConfirm: userData.passwordConfirm,
        email: userData.email,
      });

      if (response.data?.succeeded) {
        setMessage("Başarılı bir şekilde kaydoldunuz!");
        setTimeout(() => navigate("/login"), 2000); // 2 saniye sonra login sayfasına yönlendirme
      } else {
        setMessage(response.data?.message || "Kayıt işlemi başarısız oldu.");
      }
    } catch (error) {
      console.error(error);
      setMessage("Bir hata oluştu.");
    }
  };

  return (
    <div className="container d-flex justify-content-center align-items-center vh-100">
      <div className="row w-100">
        {/* Sol kısım: Görsel */}
        <div className="col-md-6 d-flex justify-content-center align-items-center">
          <img
            src="/src/Images/register.jpg"
            alt="Illustration"
            className="img-fluid"
          />
        </div>

        {/* Sağ kısım: Form */}
        <div className="col-md-6">
          <h2 className="mb-4">Kaydolun ve öğrenmeye başlayın</h2>
          {message && <div className="alert alert-info">{message}</div>} {/* Mesaj göstermek */}
          <form>
            <div className="form-group mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="Fullname"
                onChange={(e) =>
                  setUserDataState((prevState) => ({
                    ...prevState,
                    nameSurname: e.target.value,
                  }))
                }
              />
            </div>
            <div className="form-group mb-3">
              <input
                type="email"
                className="form-control"
                placeholder="Email"
                onChange={(e) =>
                  setUserDataState((prevState) => ({
                    ...prevState,
                    email: e.target.value,
                  }))
                }
              />
            </div>
            <div className="form-group mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="UserName"
                onChange={(e) =>
                  setUserDataState((prevState) => ({
                    ...prevState,
                    username: e.target.value,
                  }))
                }
              />
            </div>
            <div className="form-group mb-3">
              <input
                type="password"
                className="form-control"
                placeholder="Password"
                onChange={(e) =>
                  setUserDataState((prevState) => ({
                    ...prevState,
                    password: e.target.value,
                  }))
                }
              />
            </div>
            <div className="form-group mb-3">
              <input
                type="password"
                className="form-control"
                placeholder="PasswordConfirm"
                onChange={(e) =>
                  setUserDataState((prevState) => ({
                    ...prevState,
                    passwordConfirm: e.target.value,
                  }))
                }
              />
            </div>

            <button
              type="button"
              className="btn btn-primary w-100"
              onClick={handleRegistrationSubmit}
            >
              Kaydol
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default Register;
