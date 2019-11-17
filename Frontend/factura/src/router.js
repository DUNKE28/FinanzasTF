import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Factura from './components/Factura.vue'
import Cartera from './components/Cartera.vue'
import Login from './components/Login.vue'
import Registro from './components/Registro.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/facturas',
      name: 'facturas',
      component: Factura
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/registro',
      name: 'registro',
      component: Registro
    },
    {
      path: '/carteras',
      name: 'carteras',
      component: Cartera
    }
  ]
})
