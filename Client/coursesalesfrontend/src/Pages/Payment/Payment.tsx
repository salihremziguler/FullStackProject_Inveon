import React, { useState } from 'react';
import { loadStripe } from '@stripe/stripe-js';
import { Elements, CardElement, useStripe, useElements } from '@stripe/react-stripe-js';
import { useNavigate } from 'react-router-dom';

import './Styles/Payment.css';
import { Loader } from '../../Helper';

const stripePromise = loadStripe('pk_test_51QdsB3RpdmZBGVcBE9TvL8VWOpPd49dYOkMQfGC6v0vToMrPlrpofN62GywIDsWPeeSl6tq9jX6hao5hhmz1djU400MaKEcaUn');

function CheckoutForm() {
  const stripe = useStripe();
  const elements = useElements();
  const navigate = useNavigate();
  const [isProcessing, setIsProcessing] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setIsProcessing(true);


    const token = localStorage.getItem('token');
    if (!token) {
      setErrorMessage('Lütfen giriş yapın.');
      setIsProcessing(false);
      return;
    }

    if (!stripe || !elements) {
      setErrorMessage('Stripe yüklenemedi. Lütfen sayfayı yenileyin.');
      setIsProcessing(false);
      return;
    }

    const cardElement = elements.getElement(CardElement);
    if (!cardElement) {
      setErrorMessage('Kart bilgileri alınamadı.');
      setIsProcessing(false);
      return;
    }

    try {

      const response = await fetch('https://localhost:7154/api/Payment/Pay', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`, 
        },
        body: JSON.stringify({ paymentMethodId: 'card' }),
      });

      if (response.status === 401) {
        throw new Error('Yetkisiz erişim. Lütfen giriş yapın.');
      }

      const paymentIntent = await response.json();
      if (!paymentIntent.clientSecret) {
        throw new Error('Backend doğru bir client secret döndürmedi.');
      }

   
      const result = await stripe.confirmCardPayment(paymentIntent.clientSecret, {
        payment_method: {
          card: cardElement,
        },
      });

      if (result.error) {
        setErrorMessage(result.error.message || 'Ödeme başarısız oldu.');
      } else if (result.paymentIntent && result.paymentIntent.status === 'succeeded') {
        alert('Ödeme başarıyla tamamlandı!');
        navigate('/order');
      }
    } catch (error: any) {
      setErrorMessage(error.message || 'Ödeme işlemi sırasında bir hata oluştu.');
    } finally {
      setIsProcessing(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="payment-form">
      <h1 className="payment-title">Kart Bilgileriyle Ödeme</h1>
      <CardElement className="card-element" />
      {errorMessage && <p className="error-message">{errorMessage}</p>}
      <button className="btn btn-primary" type="submit" disabled={!stripe || isProcessing}>
        {isProcessing ? 'İşlem Yapılıyor...' : 'Ödemeyi Yap'}
      </button>
      {isProcessing && <Loader />} {/* Loader göster */}
    </form>
  );
}

function Payment() {
  return (
    <Elements stripe={stripePromise}>
      <CheckoutForm />
    </Elements>
  );
}

export default Payment;
