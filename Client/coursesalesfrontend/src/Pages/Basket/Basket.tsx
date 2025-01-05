import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useGetBasketItemsQuery, useRemoveBasketItemMutation } from '../../Api/basketApi';
import './Style/Basket.css';
import { Loader } from '../../Helper';

// ErrorMessage Bileşeni
function ErrorMessage({ message }: { message: string }) {
  return (
    <div className="error-message">
      <p>{message}</p>
    </div>
  );
}

function Basket() {
  const navigate = useNavigate();
  const { data, isLoading, error } = useGetBasketItemsQuery();
  const [removeBasketItem] = useRemoveBasketItemMutation();

  if (isLoading) return <Loader />;

  if (error) {
    const errorMessage = error.message || 'Bilinmeyen bir hata oluştu.';
    return (
      <ErrorMessage message={`Sepet verileri yüklenirken bir hata oluştu: ${errorMessage}`} />
    );
  }

  const basketItems = data || [];

  if (basketItems.length === 0) {
    return (
      <div className="basket-page">
        <div className="basket-header">
          <h1 className="basket-title">Alışveriş Sepeti</h1>
          <p className="basket-subtitle">Sepette 0 Kurs Var</p>
        </div>
        <div className="empty-basket-content">
          <img
            src="/src/Images/basket.png"
            alt="Empty basket"
            className="empty-basket-image"
          />
          <p className="empty-basket-text">Sepetiniz boş. Bir kurs bulmak için alışverişe devam edin!</p>
          <button
            className="continue-shopping-button"
            onClick={() => navigate('/')}
          >
            Alışverişe devam et
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="basket-container">
      <h2 className="basket-header-title">Sepetim</h2>
      <div className="basket-items">
        {basketItems.map((item: any) => (
          <div key={item.basketItemId} className="basket-item">
            <img
              src={`https://localhost:7154${item.image}`}
              alt={item.name}
              className="basket-item-image"
            />
            <div className="basket-item-details">
              <h3 className="basket-item-name">{item.name}</h3>
              <p className="basket-item-category"><strong>Kategori:</strong> {item.category}</p>
              <p className="basket-item-price"><strong>Fiyat:</strong> ${item.price}</p>
              <button
                className="btn btn-danger basket-item-remove-button"
                onClick={() => handleRemoveItem(item.basketItemId)}
              >
                Kaldır
              </button>
            </div>
          </div>
        ))}
      </div>

      <div className="basket-summary">
        <button
          className="btn btn-primary purchase-button"
          onClick={() => navigate('/payment')}
        >
          Satın Al
        </button>
      </div>
    </div>
  );

  async function handleRemoveItem(basketItemId: string) {
    try {
      await removeBasketItem(basketItemId).unwrap();
      alert('Ürün başarıyla kaldırıldı!');
    } catch (error) {
      const errorMessage = error.message || 'Sepetten ürün kaldırılırken bir hata oluştu.';
      alert(errorMessage);
    }
  }
}

export default Basket;
