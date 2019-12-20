import Vue from 'vue'
import Router from 'vue-router'

import { Role } from './_helpers/role'
import { authenticationService } from './_services/authentication.service';

import Home from './views/Home.vue'
import Profile from './views/Profile.vue'
import CreateEvent from './views/CreateEvent.vue'
import MyEvents from './views/MyEvents.vue'

import Login from './components/LoginPage.vue'
import Register from './components/RegisterPage.vue'
import Event from './components/Event.vue'
import EventsList from './views/EventsList.vue'


const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes : [
      {
        path: '/',
        name: 'home',
        component: Home,
        meta: {
          reload: true
        }
      },
      {
        path: '/profile',
        name: 'profile',
        component: Profile,
        meta: { authorize: [Role.User, Role.Admin] } 
      },
      {
        path: '/login',
        name: 'login',
        component: Login,
        meta: {
          reload: true
        }
      },
      {
        path: '/register',
        name: 'register',
        component: Register
      },
      {
        path: '/create',
        name: 'create',
        component: CreateEvent,
        meta: { authorize: [Role.Admin] } 
      },
      {
        path: '/event/:id',
        name: 'event',
        props: true,
        component: Event
      },
      {
        path: '/events/',
        name: 'events',
        component: EventsList,
        props: true
      },
      {
        path: '/about',
        name: 'about',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
      },
      // otherwise redirect to home
      { path: '*',redirect: '/' }
    ]
});
router.beforeEach((to, from, next) => {
  const { authorize } = to.meta;
    const currentUser = JSON.parse(authenticationService.currentUserValue);

    if (authorize) {
        if (!currentUser) {
            // not logged in so redirect to login page with the return url
            return next({ path: '/login', query: { returnUrl: to.path } });
        }
        // check if route is restricted by role
        if (authorize.length && !authorize.includes(currentUser.role)) {
            // role not authorised so redirect to home page
            return next({ path: '/' });
        }
    }
  next();
});
export default router;