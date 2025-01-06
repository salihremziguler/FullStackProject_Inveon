import { useState } from "react";
import "./App.css";
import { Header } from "../Layout";
import { Route, Routes } from "react-router-dom";

import CourseList from "../Pages/Course/CourseList";
import CourseDetail from "../Pages/Course/CourseDetail";
import Register from "../Pages/Account/Register";
import Login from "../Pages/Account/Login";
import CreateCourse from "../Pages/Course/CreateCourse";
import Basket from "../Pages/Basket/Basket";
import UpdateUser from "../Pages/Account/UpdateUser";
import Payment from "../Pages/Payment/Payment";
import Orders from "../Pages/Order/Orders";
import PurchasedCourses from "../Pages/Order/PurchasedCourses";
import Layout from "../Pages/Footer/Layout";

function App() {
  return (
    <div className="App">
      <Header></Header>
      <div className="pb-5">
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route path="/" element={<CourseList />} />
            <Route path="Course/CourseId/:courseId" element={<CourseDetail />} />
            <Route path="Register" element={<Register />} />
            <Route path="Login" element={<Login />} />
            <Route path="/add-course" element={<CreateCourse />} />
            <Route path="/basket" element={<Basket />} />
            <Route path="/updateuser" element={<UpdateUser />} />
            <Route path="/payment" element={<Payment />} />
            <Route path="/order" element={<PurchasedCourses />} />
            <Route path="/createcourse" element={<CreateCourse />} />
          </Route>
        </Routes>
      </div>
    </div>
  );
}

export default App;
