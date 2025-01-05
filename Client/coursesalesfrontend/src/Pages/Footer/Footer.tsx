import React from 'react';
import './Styles/Footer.css';

const Footer: React.FC = () => {
  return (
    <footer className="footer">
      <div className="footer-container">
        <p>&copy; {new Date().getFullYear()} My Website. All rights reserved.</p>
        <nav className="footer-nav">
          <p>Salih Remzi Güler</p>
          <p>INVEON</p>

        </nav>
      </div>
    </footer>
  );
};

export default Footer;
