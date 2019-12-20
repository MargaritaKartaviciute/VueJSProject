<template>
  <div class="card text-white bg-dark mb-3" id="app">
    <div class="image">
    <b-img v-if="event && event.image" v-bind:src="event.image" class=" img-fluid card-img-top thumbnail" fluid thumbnail  alt="image" rounded v-bind="mainProps" style="max-width: 250px;max-height: 250px"></b-img>
    <b-img v-else src="https://www.ccjays.com/wp-content/uploads/2019/01/1969/always-up-to-date-guide-to-social-media-image-sizes.png" class="img-fluid card-img-top thumbnail" fluid  thumbnail  alt="image" style="max-width: 400px;max-height: 400px" rounded v-bind="mainProps"></b-img>
    </div>
    <div class="card-body">
      <div class="sixe">
      <h5 class="card-title" v-if="event" >{{event.name}}</h5>
      <p class="card-text overflow-auto" v-if="event">{{event.description}}</p>
      </div>
      <a v-bind:href="'/event/' + event.id" class="btn btn-outline-success" v-if="event">More details!</a>
    </div>
  </div>
  
</template>
<script>
export default {
  props: ["event"],
  data() {
      return {
        mainProps: {
          center: true,
          fluidGrow: true,
        //   blank: true,
        //   blankColor: '#bbb',
          width: 600,
          height: 400,
          class: 'card-img-top'
        }
      }
    }
};
</script>
<style scoped>
.thumbnail:hover {
    position:relative;
    top:-25px;
    left:-35px;
    width:500px;
    height:auto;
    display:block;
    z-index:999;
}
#app {
  /* text-align: center; */
  /* height: 40vh; */
  width: 18rem;
}
.image{
    height: 150px
  }
  .sixe{
    height: 270px
  }
@media only screen and (min-width: 768px) {
  #app {
    /* text-align: center; */
    width: 11rem;
  }
  .image{
    height: 130px
  }
  .sixe{
    height: 230px
  }
}
@media only screen and (min-width: 1000px) {
  #app {
    /* text-align: center; */
    /* height: 25vh; */
    width: 23rem;
  }
  .image{
    height: 230px
  }
  .sixe{
    height: 120px
  }
}
</style>