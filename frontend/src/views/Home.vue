<template>
  <div class="bg-dark">
    <!-- <b-button block class="bg-dark"> -->
    <b-button class="nav-link btn btn-primary bg-dark">
      <img src="../img/list-task.svg" width="32" height="32" id="svg" title="Bootstrap" />
      <router-link :to="{ name: 'events'} " class="text-white">Event lists</router-link>
    </b-button>
    <!-- </b-button> -->
    <Map v-bind:events="events" />
  </div>
</template>

<script>
import Map from "../components/Map.vue";

import axios from "axios";
export default {
  name: "Home",
  components: {
    Map
  },
  data() {
    return {
      events: [],
      api: this.$root.$data
    };
  },
  created() {
    console.log(this.api)
    axios
      .get(this.api.value + "/events")
      .then(response => (this.events = response.data))
      .catch();
  }
};
</script>

<style lang="scss">
#svg {
  color: #fff;
  background-color: #fff;
  fill: #fff;
}
</style>