import React, { useState } from 'react';

interface BannerProps {
  onSearch: (searchTerm: string) => void;
}

function Banner({ onSearch }: BannerProps) {
  const [value, setValueState] = useState("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const searchTerm = e.target.value;
    setValueState(searchTerm); // Input değerini güncelle
    onSearch(searchTerm); // Arama terimini üst bileşene gönder
  };

  return (
    <div className='custom-banner'>
      <div className='m-auto d-flex align-items-center' style={{ width: "400px", height: "50vh" }}>
        <div className='d-flex align-items-center' style={{ width: "100%" }}>
          <input
            type='text'
            className='form-control rounded-pill'
            style={{ width: "100%", padding: "20px 20px" }}
            placeholder='Search Course'
            value={value}
            onChange={handleChange} // Her değişimde handleChange'i çağır
          />
          <span style={{ position: "relative", left: "-45px" }}>
            <i className='bi bi-search'></i>
          </span>
        </div>
      </div>
    </div>
  );
}

export default Banner;
