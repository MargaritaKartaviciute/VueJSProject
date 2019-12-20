<template>
  <div id="app">
    <div class="card-group deck">
      <div v-bind:key="event.id" v-for="event in currentEvents">
        <EventCard v-bind:event="event" />
      </div>
    </div>
    <div class="button pagination">
      <a v-on:click="first();updateCurrenEvents()">First</a>
      <a v-on:click="prevStep();updateCurrenEvents()">Prev</a>
      <a>{{currentPage + 1}}</a>
      <a v-on:click="nextStep();updateCurrenEvents()">Next</a>
      <a v-on:click="last();updateCurrenEvents()">Last</a>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import EventCard from "../components/layout/EventCard";
export default {
  name: "EventsList",
  components: {
    EventCard
  },
  data() {
    return {
      api: this.$root.$data,
      events: [],
      perPage: 4,
      currentPage: 0,
      currentEvents: []
    };
  },
  created() {
    console.log(this.api);
    axios
      .get(this.api.value + "/events")
      .then(response => (this.events = response.data))
      .catch();
  },
  watch: {
    events: function() {
      console.log(this.events);
      this.updateCurrenEvents();
    }
  },
  methods: {
    updateCurrenEvents() {
      this.currentEvents = this.events.slice(
        this.currentPage * this.perPage,
        this.currentPage * this.perPage + this.perPage
      );

      if (this.currentEvents.length == 0 && this.currentEvents > 0) {
        this.updatePage(this.currentPage - 1);
      }
    },
    updatePage(pageNumber) {
      this.currentPage = pageNumber;
      this.updateCurrenEvents();
    },
    nextStep() {
       if (Math.floor(this.events.length / this.perPage) > this.currentPage) {
        if (
          Math.floor(this.events.length / this.perPage) ==
            this.currentPage + 1 &&
          this.events.length % this.perPage == 0
        )
          this.currentPage = this.currentPage;
        else this.currentPage = this.currentPage + 1;
      }
    },
    prevStep() {
      if (this.currentPage > 0) {
        this.currentPage--;
      }
    },
    first() {
      this.currentPage = 0;
    },
    last() {
      this.currentPage = Math.floor(this.events.length / this.perPage);
    }
  }
};
</script>

<style scoped>
/* .app{
  width: 100%;
} */
#app {
  text-align: center;
  /* height: 70vh; */
}
.button {
  position: absolute;
  bottom: 0;
  align-content: center;
}
.pagination {
  display: inline-block;
}
.pagination a {
  color: white;
  float: left;
  padding: 8px 16px;
  text-decoration: none;
  transition: background-color 0.3s;
  border: 1px solid #ddd;
}
#pagination a.active {
  background-color: #4caf50;
  color: white;
  border: 1px solid #4caf50;
}
@media only screen and (min-width: 768px) {
  #app {
    text-align: center;
    height: 75vh;
  }
  .button {
    position: absolute;
    bottom: 0;
    align-content: center;
  }
  .pagination {
    display: inline-block;
    background: #4caf50;
  }
  .pagination a {
    color: white;
    float: left;
    padding: 3 10px;
    text-decoration: none;
    transition: background-color 0.3s;
    border: 1px solid #ddd;
  }
  /* #pagination a.active {
    background-color: #4caf50;
    color: white;
    border: 1px solid #4caf50;
  } */
}
@media only screen and (min-width: 1000px) {
  #app {
    text-align: center;
    height: 70vh;
  }
  .button {
    position: absolute;
    bottom: 0;
    align-content: center;
  }
  .pagination {
    display: inline-block;
  }
  .pagination a {
    color: white;
    float: left;
    padding: 3 10px;
    text-decoration: none;
    transition: background-color 0.3s;
    border: 1px solid #ddd;
  }
  #pagination a.active {
    background-color: #4caf50;
    color: white;
    border: 1px solid #4caf50;
  }
}
</style>