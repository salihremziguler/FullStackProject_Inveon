import React from 'react';
import { useGetUserOrdersQuery } from '../../Api/orderApi';

import './Styles/Order.css';
import { Loader } from '../../Helper';

function Orders() {
  const { data, isLoading, error } = useGetUserOrdersQuery();

  if (isLoading) return <Loader />; // Yükleme sırasında Loader göster
  if (error) return <p>Failed to load orders: {error.message || 'Unknown error'}</p>;

  const orders = data || [];

  return (
    <div className="orders">
      <h1>Satın Alınan Siparişler</h1>
      {orders.length === 0 ? (
        <p>Hiç siparişiniz bulunmuyor.</p>
      ) : (
        orders.map((order: any) => (
          <div key={order.OrderId} className="order">
            <h2>Sipariş ID: {order.OrderId}</h2>
            <p><strong>Sipariş Tarihi:</strong> {new Date(order.OrderDate).toLocaleDateString()}</p>
            <p><strong>Toplam Tutar:</strong> ${order.TotalAmount}</p>
            <ul>
              {order.Items.map((item: any, index: number) => (
                <li key={index}>
                  {item.CourseName} - ${item.Price}
                </li>
              ))}
            </ul>
          </div>
        ))
      )}
    </div>
  );
}

export default Orders;
