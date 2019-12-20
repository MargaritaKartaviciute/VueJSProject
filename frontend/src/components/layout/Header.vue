<template>
  <div id="navigation" class="bg-dark text-white">
    <nav class="navbar navbar-default navbar-fixed-top">
      <ul>
        <li>
          <a>
            <router-link to="/" class="text">Events</router-link>
          </a>
        </li>
        <li v-if="currentUser && currentUser.role == 'Admin'">
          <a>
            <router-link to="/create" class="text">Create</router-link>
          </a>
        </li>
        <li>
          <a v-if="currentUser">
            <router-link to="/profile" class="text">Profile</router-link>
          </a>
        </li>
        <li>
          <a>
            <router-link to="/about" class="text">About</router-link>
          </a>
        </li>
      </ul>
      <form class="form-inline my-2 my-lg-0">
        <ul>
          <li class="nav-item" v-if="currentUser">
            <a class="nav-link btn btn-outline-danger text" @click="logout">Logout</a>
          </li>
        <div v-else>
          <li class="nav-item" >
            <a>
              <router-link to="/login" class="nav-link btn btn-outline-success text">Login</router-link>
            </a>
          </li>
          <li>
            <router-link to="/register" class="nav-link btn btn-outline-warning text">Register</router-link>
          </li>
        </div>
        </ul>
      </form>
    </nav>
  </div>
</template>
<script>
import { authenticationService } from "../../_services/authentication.service";
export default {
  name: "Header",
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
#navigation {
  // display: flex;
  width: 100%;
  margin-bottom: 50px;
  ul {
    display: flex;
    list-style: none;
    padding: 0;
    margin: 0 20px 0 0;
    li {
      font-size: 2rem;
      padding: 2px 10px;
      cursor: pointer;
      float: left;
      &:hover {
        color: #7ca971;
      }
    }
  }
}
.text {
  color: #fff;
  font-size: 2.5rem;
  font-weight: bold;
  margin-bottom: 20px;
  font-family: "Dancing Script", cursive;
}
.login {
  // display: inline;
  // text-align: right;
  // margin-left:auto; margin-right:0;
  display: flex;
  justify-content: flex-end;
}
.floatright {
  float: right;
}
</style>