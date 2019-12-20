import Vue from 'vue'
import Vuex from 'vuex'
import VuexPersist from 'vuex-persist';

import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import App from './App.vue'
import router from './router'
import store from './_store/'

import { ValidationProvider, extend } from 'vee-validate';
import { required } from 'vee-validate/dist/rules';
import VueRouter from 'vue-router'

import JwPagination from 'jw-vue-pagination';
import VueSweetalert2 from 'vue-sweetalert2';

extend('required', {
  ...required,
  message: 'This field is required'
});

Vue.component('jw-pagination', JwPagination);
Vue.use(VueSweetalert2);
Vue.use(BootstrapVue)
Vue.use(VueRouter)
Vue.use(Vuex);

Vue.config.devtools = true;


Vue.config.productionTip = false

new Vue({
  components: {
    ValidationProvider
  },
  data: () => ({
    value: 'http://localhost:8000/api', //your backend api
    key: '[MAP_KEY]'
  }),
  router,
  store,
  render: h => h(App)
}).$mount('#app')
