<template>
    <div class="table">
        <form id="sendForm">
            <textarea id="sendText" rows="5" placeholder="Text to send"></textarea>
            <input type="submit" value="send" />
        </form>
        <pre id="incomming"></pre>
    </div>
</template>

<script>
    import moduleService from '@/server/moduleService'
    import testService from '@/server/testService'
    import session from '@/store/storage'
    import api from '@/server/api'
    export default {
        name: 'basetable',
        data() {
            return {
                loading: true
            }
        },
        created() {

        },
        computed: {

        },
        methods: {
        }
    }
</script>


<script type="text/javascript">
    //var start = function () {
    //    var inc = document.getElementById('incomming');
    //    var wsImpl = window.WebSocket || window.MozWebSocket;
    //    var form = document.getElementById('sendForm');
    //    var input = document.getElementById('sendText');

    //    inc.innerHTML += "connecting to server ..<br/>";

    //    window.ws = new wsImpl('ws://localhost:8082/');

    //     当数据从服务器服务中心发送后，继续向下运行过程
    //    ws.onmessage = function (evt) {
    //        if (typeof event.data === String) {
    //            console.log("Received data string:" + event.data);
    //        }
    //        else if (event.data instanceof ArrayBuffer) {
    //            var buffer = event.data;
    //            console.log("Received arraybuffer");
    //        }

    //        inc.innerHTML = evt.data + '<br/>' + inc.innerHTML;
    //    };

    //     当链接对象找到服务端成功对接后，提示正常打开
    //    ws.onopen = function () {
    //        inc.innerHTML += '.. connection open<br/>';
    //    };

    //     当链接对象未找找到服务端成功对接后，提示打开失败
    //    ws.onclose = function () {
    //        inc.innerHTML += '.. connection closed<br/>';
    //    }

    //    form.addEventListener('submit', function (e) {
    //        e.preventDefault();
    //        var obj = {};
    //        obj.Variables = [];
    //        obj.Variables.push({ VariableName: "A", BindSignalName: "SA", BindSignalValue: "5" });
    //        obj.Variables.push({ VariableName: "B", BindSignalName: "SB", BindSignalValue: "6" });
    //        obj.Variables.push({ VariableName: "C", BindSignalName: "SC", BindSignalValue: "7" });
    //        obj.Variables.push({ VariableName: "D", BindSignalName: "SD", BindSignalValue: "8" });

    //        if (input.value.indexOf("function") == -1) {
    //            input.value = "(function(){ \n    " + input.value + "\n })()";
    //        }
    //        obj.Func = input.value;
    //        var sendObj = JSON.stringify(obj);
    //        ws.send(sendObj);
    //        input.value = "";
    //    });
    //    window.onbeforeunload = function () {
    //        ws.close();
    //    }
    //}
    //window.onload = start;
</script>

