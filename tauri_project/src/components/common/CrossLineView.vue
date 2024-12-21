<template>
  <div class="container">
    <slot></slot>
    <div class="crosshair crosshair-vertical"></div>
    <div class="crosshair crosshair-horizontal"></div>
  </div>
</template>

<script>
export default {
  name: 'CrossLineView',
}
</script>

<style scoped>
.container {
  width: 320px;
  height: 320px;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  border: 1px solid cyan;
}

.crosshair {
  position: absolute;
  border-style: dashed;
  border-color: #ff0000;
}

.crosshair-vertical {
  top: 0;
  bottom: 0;
  left: 50%;
  border-left-width: 0px;
}

.crosshair-horizontal {
  left: 0;
  right: 0;
  top: 75%;
  border-top-width: 0px;
}
</style>
