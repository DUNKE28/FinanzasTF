import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Paciente from './components/Paciente.vue'
import Orden from './components/Orden.vue'
import Factura from './components/Factura.vue'
import Cartera from './components/Cartera.vue'

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
      path: '/pacientes',
      name: 'pacientes',
      component: Paciente
    },
    {
      path: '/ordenes',
      name: 'ordenes',
      component: Orden
    },
    {
      path: '/facturas',
      name: 'facturas',
      component: Factura
    },
    {
      path: '/carteras',
      name: 'carteras',
      component: Cartera
    }
  ]
})
