<template>
  <div class="parent-component">
    <nav class="navigation">
      <router-link to="/chara_cell_editor">CharaCellEditor</router-link>
      <router-link to="/base_motion_editor">BaseMotionEditor</router-link>
      <router-link to="/battle_motion_editor">BattleMotionEditor</router-link>
    </nav>
    <div class="content">
      <router-view/>
    </div>
  </div>
</template>


<script lang="ts">
import {defineComponent, onMounted} from 'vue'
import {useStore} from 'vuex'
import CharaCellEditor from "./page/CharaCellEditor.vue";
import BaseMotionEditor from "./page/BaseMotionEditor.vue";
import BattleMotionEditor from "./page/BattleMotionEditor.vue";

export default defineComponent({
  components: {CharaCellEditor, BaseMotionEditor, BattleMotionEditor},
  setup() {
    const store = useStore()

    onMounted(async () => {
      await store.dispatch('charaCell/loadState')
    })
  }
})
</script>

<style scoped>
.parent-component {
  display: flex;
  flex-direction: column;
}

.navigation {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  background-color: #333;
  z-index: 1000;
  padding: 10px;
  display: flex; /* Flexboxを有効に */
  justify-content: space-around; /* リンク間に均等な隙間を作成 */
}

.navigation a {
  text-decoration: none; /* 下線を削除（任意） */
}

.router-link-active {
  color: yellow;
}

.content {
  margin-top: 50px; /* ヘッダーの高さに応じて調整 */
  width: 100%;
  overflow: auto; /* コンテンツエリアがスクロール可能に */
  flex-grow: 1; /* コンテンツエリアが残りのスペースを占める */
  position: relative; /* ヘッダーとの重なり順を制御 */
}
</style>