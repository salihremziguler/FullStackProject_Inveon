import React from 'react';
import './Styles/Header.css';
import { NavLink } from 'react-router-dom';


function Header() {
  return (
    <div>
      <nav className="navbar navbar-expand-lg navbar-light bg-gray">
        <div className="container">
          <NavLink className="navbar-brand" to="/">
            My Galaxy Auction
          </NavLink>
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

          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav mr-auto">
              <div className="collapse navbar-collapse mr-2" id="navbarNavDarkDropdown">
                <ul className="navbar-nav">
                  <li className="nav-item dropdown">
                    <button
                      className="btn btn-dark dropdown-toggle"
                      data-bs-toggle="dropdown"
                      aria-expanded="false"
                    >
                      Menu's
                    </button>
                    <ul className="dropdown-menu dropdown-menu-dark">
                      <li>
                        <NavLink className="dropdown-item" to="/Admin/VehicleIndex">
                          Vehicle List
                        </NavLink>
                      </li>
                    </ul>
                  </li>
                </ul>
              </div>
              <li className="nav-item" style={{ marginRight: '5px' }}>
                <NavLink className="btn btn-success" to="/register">
                  Register
                </NavLink>
              </li>
              <li className="nav-item" style={{ marginRight: '5px' }}>
                <NavLink className="btn btn-success" to="/login">
                  Login
                </NavLink>
              </li>
              <li className="nav-item" style={{ marginRight: '5px' }}>
                <NavLink className="btn btn-success" to="/login">
                  Kurs Ekle
                </NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </div>
  );
}

export default Header;
