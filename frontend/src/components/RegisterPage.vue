<template>
  <div class="bg-dark text-white" id="app">
    <h2>Register</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="firstName">First Name</label>
        <ValidationProvider rules="required" v-slot="{ errors }">
          <input type="text" v-model="user.firstName" name="firstName" class="form-control" />
          <span class="invalid-feedback">{{ errors[0] }}</span>
        </ValidationProvider>
      </div>
      <div class="form-group">
        <label for="surname">Last Name</label>
        <ValidationProvider rules="required" v-slot="{ errors }">
          <input type="text" v-model="user.surname" name="surname" class="form-control" />
          <span class="invalid-feedback">{{ errors[0] }}</span>
        </ValidationProvider>
      </div>
      <div class="form-group">
        <label for="username">Username</label>
        <ValidationProvider rules="required" v-slot="{ errors }">
          <input type="text" v-model="user.username" name="username" class="form-control" />
          <span class="invalid-feedback">{{ errors[0] }}</span>
        </ValidationProvider>
      </div>
      <div class="form-group">
        <label for="password">Password</label>
        <ValidationProvider rules="required" v-slot="{ errors }">
          <input type="password" v-model="user.password" name="password" class="form-control" />
          <span class="invalid-feedback">{{ errors[0] }}</span>
        </ValidationProvider>
      </div>
      <div class="form-group">
        <button class="btn btn-outline-success" :disabled="status.registering">Register</button>
        <img
          v-show="status.registering"
          src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="
        />
        <router-link to="/login" class="btn btn-outline-danger">Cancel</router-link>
      </div>
    </form>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import { ValidationProvider, extend, validate } from "vee-validate";
import { required } from "vee-validate/dist/rules";
export default {
  components: {
    ValidationProvider
  },
  data() {
    return {
      user: {
        firstName: "",
        lastName: "",
        username: "",
        password: ""
      },
      submitted: false
    };
  },
  computed: {
    ...mapState("account", ["status"])
  },
  methods: {
    ...mapActions("account", ["register"]),
    handleSubmit() {
      this.submitted = true;
      console.log(this.user);
      var router = this.$router;
      var user = this.user;
      this.register({ user, router });
    }
  }
};
</script>
<style scoped>
#app {
  text-align: center;
  height: 70vh;
}
@media only screen and (min-width: 768px) {
  #app {
    text-align: center;
    height: 55vh;
  }
}
@media only screen and (min-width: 1000px) {
  #app {
    text-align: center;
    height: 55vh;
  }
}
</style>