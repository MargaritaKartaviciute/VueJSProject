<template>
  <div id="navigation-mobile">
    <ul>
      <li>
        <router-link to="/" class="text">Events</router-link>
      </li>
      <li>
        <router-link
          to="/create"
          class="text"
          v-if="currentUser && currentUser.role == 'Admin'"
        >Create</router-link>
      </li>
      <li>
        <router-link to="/profile" class="text" v-if="currentUser">Profile</router-link>
      </li>
      <li>
        <router-link to="/about" class="text">About</router-link>
      </li>

      <li class="nav-item" v-if="currentUser">
        <a class="text" @click="logout">Logout</a>
      </li>
      <div v-else>
        <li class="nav-item">
          <router-link to="/register" class="text">Register</router-link>
        </li>
        <li>
          <a>
            <router-link to="/login" class="text">Login</router-link>
          </a>
        </li>
      </div>
    </ul>
    <div class="social-media">
      <i class="fab fa-twitter"></i>
      <i class="fab fa-instagram"></i>
      <i class="fab fa-pinterest"></i>
      <i class="x"></i>
    </div>
  </div>
</template>

<script>
import { authenticationService } from "../../_services/authentication.service";
export default {
  name: "HeaderMobile",
  data() {
    return {
      currentUser: JSON.parse(authenticationService.currentUserValue)
    };
  },
  methods: {
    logout() {
      authenticationService.logout();
      this.$router.push("/login");
    }
  }
};
</script>

<style lang="scss" scoped>
@import url("https://fonts.googleapis.com/css?family=Dancing+Script&display=swap");
#navigation-mobile {
  padding: 20px 0 0 0;
  ul {
    list-style: none;
    width: 200px;
    padding-left: 40px;
    li {
      color: #fff;
      font-size: 2rem;
      font-weight: bold;
      margin-bottom: 20px;
      cursor: pointer;
      &:hover {
        color: #111;
        a {
          color: #fff;
          font-weight: bold;
          router-link {
            color: #fff;
            font-size: 2rem;
            font-weight: bold;
            margin-bottom: 20px;
          }
        }
      }
    }
  }
  .social-media {
    position: absolute;
    left: 40px;
    bottom: 20px;
    i {
      margin: 0 20px 0 0;
      color: #fff;
      font-size: 2rem;
    }
  }
}
.text {
  color: #fff;
  font-size: 3rem;
  font-weight: bold;
  margin-bottom: 20px;
  font-family: "Dancing Script", cursive;
}
</style>