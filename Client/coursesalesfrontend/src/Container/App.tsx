import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import CourseList from '../Pages/Course/CourseList'

import { Header } from '../Layout'
import { Route, Routes } from 'react-router-dom'
import CourseDetail from '../Pages/Course/CourseDetail'

function App() {
 

  return (
    <div className='App'>
 <Header></Header>

 <div className='pb-5' >
        <Routes>
        <Route path='/' element={<CourseList></CourseList>} ></Route>
          <Route path='Course/CourseId/:courseId' element={<CourseDetail></CourseDetail>} ></Route>
         
        </Routes>
      </div>

    </div>
   
    
  )
}

export default App
