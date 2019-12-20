<template>
  <div id="app">
    <HeaderMobile />
    <div class="content" :class="{'open':showNav}">
      <div class="top-bar">
        <div id="navigation-icon" v-if="mobileView" @click="showNav = !showNav">
          <i class="fas fa-bars"></i>
        </div>
        <Header v-if="!mobileView" />
        <div v-if="alert.message" :class="`alert ${alert.type}`">{{alert.message}}</div>
        <router-view></router-view>
        <footer class="footer">
          <i class="fas fa-copyright"></i>

          This website made by Margarita Kartavičiūtė
        </footer>
      </div>
    </div>
  </div>
</template>
<script>
import Header from "./components/layout/Header";
import HeaderMobile from "./components/layout/HeaderMobile";
import { authenticationService } from "./_services/authentication.service";
import Role from "./_helpers/role";
import { mapState, mapActions } from "vuex";
export default {
  name: "app",
  components: {
    Header,
    HeaderMobile
  },
  data() {
    return {
      currentUser: null,
      mobileView: true,
      showNav: false
    };
  },
  computed: {
    ...mapState({
      alert: state => state.alert
    }),
    isAdmin() {
      return this.currentUser && this.currentUser.role === Role.Admin;
    }
  },
  created() {
    this.handleView();
    window.addEventListener("resize", this.handleView);
    authenticationService.currentUser.subscribe(x => (this.currentUser = x));
  },
  methods: {
    ...mapActions({
      clearAlert: "alert/clear"
    }),
    handleView() {
      this.mobileView = window.innerWidth <= 990;
    }
  },
  watch: {
    $route(to, from) {
      this.clearAlert();
    }
  }
};
</script>

<style lang="scss">
@import url("https://use.fontawesome.com/releases/v5.9.0/css/all.css");
@import url("https://fonts.googleapis.com/css?family=Dancing+Script&display=swap");
* {
  font-size: 1rem;
}
body {
  width: 100%;
  height: 100vh;
  margin: 0;
  padding: 0;
  font-family: "Segoe UI", Tahoma;
  background-color: #7ca971;
}
#app {
  position: relative;
  width: calc(100% - 20px);
  height: calc(100vh - 20px);
  padding: 10px;
  color: #333;
  overflow: hidden;
}
.top-bar {
  //   display: flex;
  width: 100%;
}
#navigation-icon {
  padding: 10px 10px 20px;
  margin-right: 10px;
  cursor: pointer;
  i {
    font-size: 2rem;
  }
}
.content {
  position: absolute;
  top: 10px;
  width: calc(100% - 60px);
  height: calc(100vh - 60px);
  padding: 20px;
  background-color: #fff;
  border-radius: 30px;
  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);
  transition: 1s transform cubic-bezier(0, 0.12, 0.14, 1);
}
.open {
  transform: translateX(300px);
}
.footer {
  position: absolute;
  bottom: 0;
  font-size: 1.5rem;
  background: #73b363;
  text-align: center;
  font-family: "Dancing Script", cursive;
  color: #fff;
}
</style>
