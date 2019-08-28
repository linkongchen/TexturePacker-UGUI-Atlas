# TexturePacker-UGUI-Atlas
TexturePacker自动生成UGUI纹理贴图+生成altas工具

准备工作,首先需要安装TexturePacker-5.1.0-x64.msi,我使用的版本,因为软件付费,不需要担心水印问题.
其次,在unity中安装插件TexturePackerImporter.
需求方案是针对美术某目录下面所有子文件夹,对每一个文件夹里面所有素材做一个图集,生成spritesheet的metaData,方便程序用代码去控制调用,习惯NGUI的程序可以理解为atlas.


CMD,codeandweb.com,UGUIAtals三个文件夹放在Assets/Editor目录下
DrawImageManager放在Assets/Scripts目录下即可
