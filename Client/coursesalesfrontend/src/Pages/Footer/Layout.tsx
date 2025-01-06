import React from 'react';
import { Outlet } from 'react-router-dom'; // Dinamik içerik için
import Footer from './Footer';
import "./Styles/Loayout.css"

const Layout: React.FC = () => {
  return (
    <div className="layout">
      <main className="content">
        <Outlet /> {/* Sayfa içeriği burada yüklenecek */}
      </main>
      <Footer /> {/* Footer her sayfada gösterilecek */}
    </div>
  );
};

export default Layout;
