import React, { useEffect } from "react";
import "./Styles/Header.css";
import { NavLink, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../Storage/store";
import { InitialState, setLoggedInUser } from "../Storage/Redux/authenticationSlice";

function Header() {
  const userStore = useSelector((state: RootState) => state.authenticationStore);
  const token = localStorage.getItem("token");
  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    console.log("User Store:", userStore);
  }, [userStore]);

  const handleLogout = () => {
    localStorage.removeItem("token");
    dispatch(setLoggedInUser({ ...InitialState }));
    navigate("/");
  };

  return (
    <div>
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="container">
          {/* Logo */}
          <NavLink className="navbar-brand" to="/">
            <span style={{ color: "#A435F0" }}>I</span>nveon
          </NavLink>

          {/* Navbar Toggler for Mobile */}
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>

          {/* Navbar Links */}
          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav me-auto">
              {/* Kullanıcı Adı */}
              {userStore?.name && (
                <li className="nav-item">
                  <a className="nav-link" href="#">
                    {userStore.name}
                  </a>
                </li>
              )}
            </ul>

            <ul className="navbar-nav">
              {/* Siparişlerim */}
              <li className="nav-item me-2">
                <NavLink className="btn btn-link" to="/order">
                  <i className="fas fa-box"></i> Siparişlerim
                </NavLink>
              </li>

              {/* Sepet */}
              <li className="nav-item me-2">
                <NavLink className="btn btn-link" to="/basket">
                  <i className="fas fa-shopping-cart"></i> Sepet
                </NavLink>
              </li>

              {/* Kurs Ekleme Butonu */}
              {userStore.role === "salih" && (
                <li className="nav-item me-2">
                  <NavLink className="btn btn-success" to="/createcourse">
                    <i className="fas fa-plus-circle"></i> Kurs Ekle
                  </NavLink>
                </li>
              )}

              {/* Bilgilerim */}
              {userStore?.name && (
                <li className="nav-item me-2">
                  <NavLink className="btn btn-info" to="/updateuser">
                    <i className="fas fa-user"></i> Bilgilerim
                  </NavLink>
                </li>
              )}

              {/* Kullanıcı Giriş ve Çıkış */}
              {userStore?.name ? (
                <li className="nav-item me-2">
                  {/* Çıkış Yap Butonu */}
                  <button className="btn btn-outline-dark" onClick={handleLogout}>
                    Çıkış Yap
                  </button>
                </li>
              ) : (
                <>
                  <li className="nav-item me-2">
                    <NavLink className="btn btn-outline-dark" to="/login">
                      Oturum Aç
                    </NavLink>
                  </li>
                  <li className="nav-item">
                    <NavLink className="btn btn-dark text-white" to="/register">
                      Kaydol
                    </NavLink>
                  </li>
                </>
              )}
            </ul>
          </div>
        </div>
      </nav>
    </div>
  );
}

export default Header;
